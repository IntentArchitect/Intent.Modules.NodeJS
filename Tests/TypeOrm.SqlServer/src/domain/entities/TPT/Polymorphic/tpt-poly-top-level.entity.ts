import { TptPoly_RootAbstract } from './tpt-poly-root-abstract.entity';
import { Entity, PrimaryGeneratedColumn, Column, OneToMany } from 'typeorm';

@Entity('tpt_poly_top_level')
export class TptPoly_TopLevel {
  @PrimaryGeneratedColumn('uuid')
  id: string;

  @Column()
  topField: string;

  @OneToMany(() => TptPoly_RootAbstract, (poly_RootAbstracts) => poly_RootAbstracts.poly_TopLevel, { cascade: ['insert', 'update'] })
  poly_RootAbstracts: TptPoly_RootAbstract[];

  @Column({ nullable: true })
  createdBy?: string;

  @Column({ nullable: true })
  createdDate?: Date;

  @Column({ nullable: true })
  lastModifiedBy?: string;

  @Column({ nullable: true })
  lastModifiedDate?: Date;
}