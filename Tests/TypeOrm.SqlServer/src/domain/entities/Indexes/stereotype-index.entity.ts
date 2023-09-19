import { Entity, Index, PrimaryGeneratedColumn, Column } from 'typeorm';

@Entity('stereotype_index')
@Index('GroupedIndexField', ['groupedIndexFieldA', 'groupedIndexFieldB'], { unique: true })
export class StereotypeIndex {

  @PrimaryGeneratedColumn('uuid')
  id?: string;
  @Column()
  @Index()
  defaultIndexField: string;

  @Column()
  @Index('CustomIndexField')
  customIndexField: string;

  @Column()
  groupedIndexFieldA: string;

  @Column()
  groupedIndexFieldB: string;

  @Column({ nullable: true })
  createdBy?: string;

  @Column({ nullable: true })
  createdDate?: Date;

  @Column({ nullable: true })
  lastModifiedBy?: string;

  @Column({ nullable: true })
  lastModifiedDate?: Date;
}