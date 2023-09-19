import { M_SelfReferenceBiNav } from './m-self-reference-bi-nav.entity';
import { Entity, PrimaryGeneratedColumn, Column, ManyToOne } from 'typeorm';

@Entity('m_self_reference_bi_nav')
export class M_SelfReferenceBiNav {

  @PrimaryGeneratedColumn('uuid')
  id?: string;
  @Column()
  selfRefBiNavAttr: string;

  @ManyToOne(() => M_SelfReferenceBiNav, { cascade: ['insert', 'update'] })
  m_SelfReferenceBiNav?: M_SelfReferenceBiNav;

  @Column({ nullable: true })
  createdBy?: string;

  @Column({ nullable: true })
  createdDate?: Date;

  @Column({ nullable: true })
  lastModifiedBy?: string;

  @Column({ nullable: true })
  lastModifiedDate?: Date;
}